using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Data.Repositories
{
    public class DataRepository<T> : IDataRepository<T>
    {
        //create a reference to the Firestore collection -> if not exists, it will be created automatically when a document is added
        private readonly CollectionReference _collectionReference;
        private readonly ILogger<DataRepository<T>> _logger;

        public DataRepository(FirestoreDb firestoreDb, ILogger<DataRepository<T>> logger)
        {
            _collectionReference = firestoreDb.Collection("jobs");
            _logger = logger;
        }

        public async Task AddJobAsync(string id, T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "The entity to add cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException(
                        "Document ID cannot be null or empty.",
                        nameof(id));
                }
                DocumentReference docRef = _collectionReference.Document(id);

                await _collectionReference.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add document ");
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                QuerySnapshot snapshot = await _collectionReference.GetSnapshotAsync();
                List<T> entities = new List<T>();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        T entity = document.ConvertTo<T>();
                        entities.Add(entity);
                    }
                }
                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all documents");
                throw;
            }
        }

        public async Task<T?> GetByIdAndTypeAsync(string id, int jobSourceType)
        {
            try
            {
                //validate the id parameter is not null or empty
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("The document ID cannot be null or empty.", nameof(id));
                }

                Query query = _collectionReference
                        .WhereEqualTo("ExternalJobId", id)
                        .WhereEqualTo("JobSourceType", jobSourceType);

                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                if (snapshot.Count == 0)
                {
                    return default;
                }

                return snapshot.Documents
                            .First()
                            .ConvertTo<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get document with ID {DocumentId}: ", id);
                throw;
            }
        }
    }
}

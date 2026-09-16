using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Data.Models
{
    [FirestoreData]
    public class JobPosting
    {
        [FirestoreProperty]
        public string ExternalJobId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Title { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Company { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Location { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Source { get; set; } = string.Empty;
        [FirestoreProperty]
        public string JobUrl { get; set; } = string.Empty;
    }
}

# JobPulse Repository Setup

## 1. Overview
- Setup Repository using Generic and DI registration pattern.
- loose coupling and high cohesion.
- Split the code into multiple layers for better maintainability and testability.

## 2. Repository Structure
- Repository Layer: Responsible for data access and persistence.
- Service Layer: Contains business logic and orchestrates data access.

## 3. Repository Implementation
- Use interfaces to define repository contracts in service layer
- Implement repository classes in the repository layer
- The interface or even service layer could not know the detail (Ex: how to call the firestore, fetch data, etc.)
- Create logger to handle the error and preserve stack trace for debugging and troubleshooting.

## 4. Future Plan
- Will implement the mock and unit test for each repository and service layer.
- Will continuous update the document if any change
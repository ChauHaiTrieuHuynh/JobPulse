# JobPulse Configuration

## 1. Overview
- Purpose of this configuration layer
- Configuration ownership: Worker as the composition root
- Split the file easy to maintain and understand.

## 2. Configuration Structure
- Firebase Setting
- Job Polling Setting
- Email Setting
- Job Source Setting

## 3. Environment Configuration
- appsetting.json
- Environment -specific override stragergy

## 4. Firebase / Firestore 
- Using Cloud Firestore standard 
- Firestore DB registered as a Singleton (safe thread) - common using
- Firebase configuration init by the Worker host

## 5. DI
- Configuration binding
- Firebase service registration
- Application service registration
- Service lifetime decisions (Singleton, Scoped, Transient)

## 6. Secret Management
- firebase.json excluded from Git
- Secrets never stored in appsettings.json
- Local credentials from developement
- Managed secrets storeage / Key Vault for future production deployment

## 7. Current Configuration
- Polling interval: 60 minutes
- Job source: Indeed and LinkedIn
- Email setting: configurable
- Worker project running in singleton -> solution is creating each polling scope to handle task -> dispose immediatelly after task done -> wait 60 minutes -> begin new polling scope



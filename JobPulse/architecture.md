# JobPulse Architecture

## 1. Overview
Short description of the architecture, including the main components and their interactions.

## 2. Solution Structure
Description of the solution structure:

- JobPulse.Worker
- JobPulse.Application
- JobPulse.Infrastructure

## 3. Component Responsibilities
Each component's responsibilities:

## 4. Dependency Flow
Component dependencies and their flow:

## 5. Processing Flow
Main flow: 

Job Source
→ Collect
→ Normalize
→ Filter
→ Duplicate Check
→ Save
→ Notify

## 6. Architecture Decisions
Essential decisions and their rationale, for example:
- Worker Service instead of Web API
- Firebase for MVP persistence
- No frontend for MVP
# WpfBackgroundServiceNet8

.NET 8 application for managing background tasks in a desktop context (WPF).
The application uses the BackgroundWorkerInitializer class to initialize background operations; the BackgroundWorker class is responsible for executing the actual operations, logging the host lifecycle states.
In the project, the following cases are also managed:
1) Prevention of background activity execution during application shutdown;
2) Management of scenarios where a long-running operation needs to be executed at regular intervals and lasts longer than the expected interval.

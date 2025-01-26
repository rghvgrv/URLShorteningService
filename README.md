# URL Shortening Service

A simple URL shortening service built with C# and .NET, allowing users to convert long URLs into shorter using custom logic, more manageable links. 

## Features

- **Shorten URLs**: Generate unique short links for long URLs.
- **Redirect**: Navigate to the original URL using the shortened link.
- **Persistent Storage**: Stores URL mappings in a JSON file for data persistence.

## Project Structure

- **Controllers/**: Contains the `UrlShortenerController` responsible for handling HTTP requests.
- **Models/**: Defines the `UrlMapping` model representing the relationship between short codes and original URLs.
- **Repositories/**: Implements the `UrlRepository` for data access and storage operations.
- **Services/**: Includes the `UrlShortenerService` containing the core logic for URL shortening and retrieval.
- **Properties/**: Contains assembly information and application properties.
- **Program.cs**: The entry point of the application.
- **appsettings.json**: Configuration file for application settings.
- **urlMapping.json**: JSON file used for storing URL mappings.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

## Getting Started

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/rghvgrv/URLShorteningService.git
   cd URLShorteningService
   ```

2. **Build the Application**:
   ```bash
   dotnet build
   ```

3. **Run the Application**:
   ```bash
   dotnet run
   ```

   The application will start, and you can access it at `http://localhost:5000`.

## Usage

- **Shorten a URL**:
  - Send a POST request to `/shorten` with a JSON body containing the `originalUrl`.
  - Example:
    ```json
    {
      "originalUrl": "https://example.com/very/long/url"
    }
    ```
  - The response will include the shortened URL.

- **Redirect to Original URL**:
  - Access the shortened URL in your browser.
  - The service will redirect you to the original URL.

## Configuration

- **Storage**:
  - The application uses `urlMapping.json` to store URL mappings.
  - Ensure this file has the necessary read/write permissions.

- **Base URL**:
  - The base URL for shortened links is configured in `appsettings.json`.
  - Update the `BaseUrl` property to match your deployment domain.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

## Project Repo
https://roadmap.sh/projects/url-shortening-service

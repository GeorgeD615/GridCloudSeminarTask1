<body>
    <h1>Dockerized .NET Minimal API</h1>
    <p>This project demonstrates a simple .NET Minimal API with SQLite running in Docker containers.</p>
    <h2>Available Endpoints</h2>
    <ul>
        <li><strong>/</strong> - Returns a list of available endpoints.</li>
        <li><strong>/users</strong> - Retrieves all users from the database.</li>
        <li><strong>/users/create?name=John</strong> - Creates a new user with the given name.</li>
        <li><strong>/users/delete-all</strong> - Deletes all users from the database.</li>
    </ul>
    <h2>Setup and Run</h2>
    <ol>
        <li>Clone the repository:</li>
        <pre><code>git clone &lt;repo_url&gt;</code></pre>
        <li>Navigate to the project folder:</li>
        <pre><code>cd &lt;repo_folder&gt;</code></pre>
        <li>Build and start the containers:</li>
        <pre><code>docker-compose up --build</code></pre>
        <li>API will be available at:</li>
        <pre><code>http://localhost:5000</code></pre>
    </ol>
    <h2>Verify Data Persistence</h2>
    <p>To confirm that SQLite data persists after restarting containers:</p>
    <ol>
        <li>Create a new user:</li>
        <pre><code>http://localhost:5000/users/create?name=John</code></pre>
        <li>Verify the user is stored:</li>
        <pre><code>http://localhost:5000/users</code></pre>
        <li>Stop and remove containers:</li>
        <pre><code>docker-compose down</code></pre>
        <li>Restart containers:</li>
        <pre><code>docker-compose up</code></pre>
        <li>Check if the user still exists:</li>
        <pre><code>http://localhost:5000/users</code></pre>
    </ol>
    <p>If the user is still there, the database data is persistent.</p>
</body>
</html>

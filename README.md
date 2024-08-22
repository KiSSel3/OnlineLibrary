# OnlineLibrary

Entry task for <strong>.NET</strong> internship at <strong>Modsen</strong> company.

<h1>Getting Started</h1>
<p>Follow these steps to run the Angular project locally.</p>

<h2>Prerequisites</h2>
<ul>
  <li><a href="https://nodejs.org/" target="_blank">Node.js</a> (LTS version recommended)</li>
  <li>Angular CLI installed globally:</li>
</ul>
<pre><code>npm install -g @angular/cli</code></pre>

<h2>Installation</h2>
<ol>
  <li>Clone the repository and navigate to the project directory:</li>
  <pre><code>git clone &lt;repository-url&gt;
cd &lt;project-directory&gt;</code></pre>
  <li>Install dependencies:</li>
  <pre><code>npm install</code></pre>
</ol>

<h2>Running the Project</h2>
<p>Start the development server:</p>
<pre><code>ng serve</code></pre>

<h1>Getting Started</h1>
<p>Follow these steps to run the ASP.NET Core API project locally.</p>

<h2>Prerequisites</h2>
<ul>
  <li><a href="https://dotnet.microsoft.com/download" target="_blank">.NET SDK</a> (Ensure you have the latest version installed)</li>
  <li>An IDE or text editor</li>
</ul>

<h2>Installation</h2>
<ol>
  <li>Clone the repository and navigate to the project directory:</li>
  <pre><code>git clone &lt;repository-url&gt;
cd &lt;project-directory&gt;</code></pre>
  <li>Restore the required packages:</li>
  <pre><code>dotnet restore</code></pre>
</ol>

<h2>Updating the Database Connection String</h2>
<p>To connect the API to your database, update the connection string in the <code>appsettings.json</code> file:</p>
<pre><code>"ConnectionStrings": {
  "ConnectionString": "Host=localhost;Port=5432;Database=OnlineLibraryDB;Username=name;Password=password"
}</code></pre>
<p>Modify the values as needed to match your database setup.</p>

<h2>Running the Project</h2>
<p>Run the API project using the following command:</p>
<pre><code>dotnet run</code></pre>
<p>By default, the API will be accessible at <a href="http://localhost:7295" target="_blank">http://localhost:7295</a>.</p>

<h2>Running with Visual Studio</h2>
<ol>
  <li>Open the project in Visual Studio.</li>
  <li>Press <code>F5</code> to build and run the project. The API will start, and the Swagger UI will be available at <a href="http://localhost:7295/swagger" target="_blank">http://localhost:7295/swagger</a>.</li>
</ol>

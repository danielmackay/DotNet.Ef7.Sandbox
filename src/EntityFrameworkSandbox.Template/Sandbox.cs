﻿using EntityFramework.Sandbox.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFramework.Sandbox;

public class Sandbox
{
    private readonly BloggingContextInitialiser _initializer;
    private readonly BloggingContext _db;
    private readonly ILogger<Sandbox> _logger;

    public Sandbox(BloggingContextInitialiser contextInitialiser, BloggingContext context, ILogger<Sandbox> logger)
    {
        _initializer = contextInitialiser;
        _db = context;
        _logger = logger;
    }

    public async Task Run()
    {
        await Initialize();
        await RunQueries();
        await RunCommands();
    }

    private async Task Initialize()
    {
        _logger.LogInformation("Initializing DB");
        await _initializer.InitialiseAsync();

        _logger.LogInformation("Seeding DB");
        await _initializer.SeedAsync();
    }

    private async Task RunQueries()
    {
        _logger.LogInformation("Getting Posts...");
        var posts = await _db.Posts.ToListAsync();
        foreach (var post in posts)
            _logger.LogInformation(post.ToString());

        _logger.LogInformation("Getting Blogs...");
        var blogs = await _db.Blogs.ToListAsync();
        foreach (var blog in blogs)
            _logger.LogInformation(blog.ToString());

        // NOTE: Further DB queries go here
    }

    private Task RunCommands()
    {
        return Task.CompletedTask;

        // NOTE: Further DB commands go here

    }
}
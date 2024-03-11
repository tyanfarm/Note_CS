using MigrationExample;

using var dbcontext = new WebContext();
dbcontext.Database.EnsureCreated();
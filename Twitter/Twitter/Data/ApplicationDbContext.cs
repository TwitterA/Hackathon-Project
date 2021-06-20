using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Models;

namespace Twitter.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
/*        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TweetModel>().HasData(new TweetModel() { Id = 1, Content="test", Time="10-10-2020" ,UserId= "35606eb1-8062-420c-8eb2-fe8eaa26c73f", ParentId = 2 });
            modelBuilder.Entity<TweetModel>().HasData(new TweetModel() { Id = 2, Content = "test", Time = "10-10-2020", UserId = "35606eb1-8062-420c-8eb2-fe8eaa26c73f", ParentId = 2 });
            base.OnModelCreating(modelBuilder);
        }*/
        public DbSet<TweetModel> Tweets { get; set; }
        public DbSet<FavoriteModel> Favorites { get; set; }
        public DbSet<FollowingModel> Following { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FollowingModel>().HasKey(table => new { table.Followed_id, table.Follower_id });
        }

    }
}


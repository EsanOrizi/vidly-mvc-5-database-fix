namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {

            Sql(@"
               INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'45bad897-e3fc-491f-83b5-ae0281ecb111', N'guest@vidly.com', 0, N'AO1D49/IIU+d75eJ3syT1QB21WJ8kxzvLpypjcdI5ZzcdK65ICDXjmbD/dC6/iXU+g==', N'3a4a58bd-38d2-453d-9407-8f68149d4a7e', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
               INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'be611e73-4a69-48e5-aaed-a8fccdeb4eed', N'admin@vidly.com', 0, N'ACY5lUXCyoSC5RbDz/fSWT2yDPWHT9nyUEneIc1PcDXKwd4H4xnc9swWJbI7yNg9uA==', N'ef92d817-a810-4200-ad98-afc841fb5550', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
               
               INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a57e5a68-6bfe-4618-b9fc-329cd30eaae8', N'CanManageMovies')
                
               INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'be611e73-4a69-48e5-aaed-a8fccdeb4eed', N'a57e5a68-6bfe-4618-b9fc-329cd30eaae8')

               ");


        }
        
        public override void Down()
        {
        }
    }
}

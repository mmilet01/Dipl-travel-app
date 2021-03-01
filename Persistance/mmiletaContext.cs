using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistance
{
    public partial class mmiletaContext : DbContext
    {
        public mmiletaContext()
        {
        }

        public mmiletaContext(DbContextOptions<mmiletaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<Counter> Counter { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Memories> Memories { get; set; }
        public virtual DbSet<MemoryImages> MemoryImages { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UserRelationshipStatus> UserRelationshipStatus { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersImages> UsersImages { get; set; }
        public virtual DbSet<UsersInAgroup> UsersInAgroup { get; set; }
        public virtual DbSet<UsersRelationship> UsersRelationship { get; set; }
        public virtual DbSet<UsersTaggedOnMemory> UsersTaggedOnMemory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=mmileta;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key)
                    .HasName("CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK__groups__88C1034DBCCE58D0");

                entity.ToTable("groups");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("groupName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.StateName)
                    .HasName("IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.HasIndex(e => new { e.StateName, e.ExpireAt })
                    .HasName("IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Memories>(entity =>
            {
                entity.HasKey(e => e.MemoryId)
                    .HasName("PK__memories__96A15C453954E06C");

                entity.ToTable("memories");

                entity.Property(e => e.MemoryId).HasColumnName("memoryId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("date");

                entity.Property(e => e.IsPrivate).HasColumnName("isPrivate");

                entity.Property(e => e.MemoryDescription)
                    .IsRequired()
                    .HasColumnName("memoryDescription")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Memories)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Memories_Users");
            });

            modelBuilder.Entity<MemoryImages>(entity =>
            {
                entity.HasKey(e => e.MemoryImageId)
                    .HasName("PK__memoryIm__283B4828AC616ABB");

                entity.ToTable("memoryImages");

                entity.Property(e => e.MemoryImageId).HasColumnName("memoryImageId");

                entity.Property(e => e.BelongsTo).HasColumnName("belongsTo");

                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .HasColumnName("photoPath")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BelongsToNavigation)
                    .WithMany(p => p.MemoryImages)
                    .HasForeignKey(d => d.BelongsTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemoryImages_Users");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__messages__4808B9938948D2FB");

                entity.ToTable("messages");

                entity.Property(e => e.MessageId).HasColumnName("messageId");

                entity.Property(e => e.FromId).HasColumnName("fromId");

                entity.Property(e => e.MessageBody)
                    .IsRequired()
                    .HasColumnName("messageBody")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReadAt)
                    .HasColumnName("readAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.SentAt)
                    .HasColumnName("sentAt")
                    .HasColumnType("date");

                entity.Property(e => e.ToGroupId).HasColumnName("toGroupId");

                entity.Property(e => e.ToId).HasColumnName("toId");

                entity.HasOne(d => d.From)
                    .WithMany(p => p.MessagesFrom)
                    .HasForeignKey(d => d.FromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessagesFrom_Users");

                entity.HasOne(d => d.ToGroup)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ToGroupId)
                    .HasConstraintName("FK_Group_Users");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.MessagesTo)
                    .HasForeignKey(d => d.ToId)
                    .HasConstraintName("FK_MessagesTo_Users");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable("notificationType");

                entity.Property(e => e.NotificationTypeId).HasColumnName("notificationTypeId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("PK__notifica__4BA5CEA98CE48858");

                entity.ToTable("notifications");

                entity.Property(e => e.NotificationId).HasColumnName("notificationId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("date");

                entity.Property(e => e.FromId).HasColumnName("fromId");

                entity.Property(e => e.IsRecieved).HasColumnName("isRecieved");

                entity.Property(e => e.NotificationTypeId).HasColumnName("notificationTypeId");

                entity.Property(e => e.ToGroupId).HasColumnName("toGroupId");

                entity.Property(e => e.ToId).HasColumnName("toId");

                entity.HasOne(d => d.From)
                    .WithMany(p => p.NotificationsFrom)
                    .HasForeignKey(d => d.FromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationFrom_Users");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_NotificationType");

                entity.HasOne(d => d.ToGroup)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.ToGroupId)
                    .HasConstraintName("FK_GroupNotification_Users");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.NotificationsTo)
                    .HasForeignKey(d => d.ToId)
                    .HasConstraintName("FK_NotificationTo_Users");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat)
                    .HasName("IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score })
                    .HasName("IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<UserRelationshipStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__userRela__36257A187926388E");

                entity.ToTable("userRelationshipStatus");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.StatusText)
                    .IsRequired()
                    .HasColumnName("statusText")
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__users__CB9A1CFF19500154");

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LastActivityTimeStamp)
                    .HasColumnName("lastActivityTimeStamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordField)
                    .IsRequired()
                    .HasColumnName("passwordField")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<UsersImages>(entity =>
            {
                entity.HasKey(e => e.UserImageId)
                    .HasName("PK__usersIma__8480DF35A7CE6092");

                entity.ToTable("usersImages");

                entity.Property(e => e.UserImageId).HasColumnName("userImageId");

                entity.Property(e => e.BelongsTo).HasColumnName("belongsTo");

                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .HasColumnName("photoPath")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BelongsToNavigation)
                    .WithMany(p => p.UsersImages)
                    .HasForeignKey(d => d.BelongsTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserImages_Users");
            });

            modelBuilder.Entity<UsersInAgroup>(entity =>
            {
                entity.HasKey(e => e.UsersGroupId)
                    .HasName("PK__usersInA__AE7DDD8B2031BA63");

                entity.ToTable("usersInAGroup");

                entity.Property(e => e.UsersGroupId).HasColumnName("usersGroupId");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UsersInAgroup)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersInAgroup)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersGroup_Users");
            });

            modelBuilder.Entity<UsersRelationship>(entity =>
            {
                entity.HasKey(e => e.RelationShip)
                    .HasName("PK__usersRel__711C4A15FCEEB959");

                entity.ToTable("usersRelationship");

                entity.Property(e => e.RelationShip).HasColumnName("relationShip");

                entity.Property(e => e.FirstUserId).HasColumnName("firstUserId");

                entity.Property(e => e.RelationshipStatus).HasColumnName("relationshipStatus");

                entity.Property(e => e.SecondUserId).HasColumnName("secondUserId");

                entity.HasOne(d => d.FirstUser)
                    .WithMany(p => p.UsersRelationshipFirstUser)
                    .HasForeignKey(d => d.FirstUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FirstUser_Users");

                entity.HasOne(d => d.RelationshipStatusNavigation)
                    .WithMany(p => p.UsersRelationship)
                    .HasForeignKey(d => d.RelationshipStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRelationshipStatus_Stauts");

                entity.HasOne(d => d.SecondUser)
                    .WithMany(p => p.UsersRelationshipSecondUser)
                    .HasForeignKey(d => d.SecondUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SecondUser_Users");
            });

            modelBuilder.Entity<UsersTaggedOnMemory>(entity =>
            {
                entity.HasKey(e => e.TagId)
                    .HasName("PK__usersTag__50FC015783197F91");

                entity.ToTable("usersTaggedOnMemory");

                entity.Property(e => e.TagId).HasColumnName("tagId");

                entity.Property(e => e.MemoryId).HasColumnName("memoryId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Memory)
                    .WithMany(p => p.UsersTaggedOnMemory)
                    .HasForeignKey(d => d.MemoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaggedMemory_Users");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersTaggedOnMemory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaggedUsers_Memory");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

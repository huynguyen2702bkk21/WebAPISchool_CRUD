namespace WebAPISchoolTest.Infrastructure.EntityConfigurations.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}

namespace WebAPISchoolTest.Infrastructure.EntityConfigurations.Students
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Đặt khóa chính
            builder.HasKey(s => s.StudentUUID);

            // Định cấu hình các thuộc tính
            builder.Property(s => s.Name)
                   .IsRequired() // Bắt buộc
                   .HasMaxLength(100); // Đặt độ dài tối đa cho tên học sinh

            builder.Property(s => s.Gender)
                   .HasMaxLength(10);
                
            builder.Property(s => s.Age);

            builder.Property(s => s.GPA)
                   .IsRequired();

            // Cấu hình mối quan hệ giữa Student và Class
            builder.HasOne<Class>() // Khai báo Class mà không cần thuộc tính Class trong Student
                   .WithMany() // Class có nhiều Students
                   .HasForeignKey(s => s.ClassID) // Khóa ngoại
                   .OnDelete(DeleteBehavior.Cascade); // Thiết lập xóa cascade
        }
    }
}

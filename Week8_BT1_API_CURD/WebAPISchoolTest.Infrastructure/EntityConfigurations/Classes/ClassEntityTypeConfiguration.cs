namespace WebAPISchoolTest.Infrastructure.EntityConfigurations.Classes
{
    public class ClassEntityTypeConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {

            // Đặt khóa chính
            builder.HasKey(c => c.ClassID);

            // Định cấu hình các thuộc tính
            builder.Property(c => c.ClassName)
                .IsRequired() // Bắt buộc
                .HasMaxLength(100); // Đặt độ dài tối đa cho tên lớp

            builder.HasOne<Teacher>() // Khai báo Teacher mà không cần thuộc tính Teacher trong Class
                .WithMany() // Teacher có nhiều Classes
                .HasForeignKey(c => c.TeacherID) // Khóa ngoại
                .OnDelete(DeleteBehavior.Cascade); // Thiết lập xóa cascade
        }
    }
}

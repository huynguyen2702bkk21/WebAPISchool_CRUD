namespace WebAPISchoolTest.Infrastructure.EntityConfigurations.Teachers
{
    public class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            // Đặt khóa chính
            builder.HasKey(t => t.TeacherID);

            // Định cấu hình các thuộc tính
            builder.Property(t => t.TeacherID)
                .ValueGeneratedOnAdd(); // Tự động tạo TeacherID

            builder.Property(t => t.Name)
                .IsRequired() // Bắt buộc
                .HasMaxLength(100); // Đặt độ dài tối đa cho tên

            builder.Property(t => t.Age)
                .IsRequired(); // Bắt buộc
        }
    }
}

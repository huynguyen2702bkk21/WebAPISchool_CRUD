namespace WebAPISchoolTest.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly List<User> _users = new(); // Thay bằng cơ sở dữ liệu trong thực tế
        private readonly string _secretKey;
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _secretKey = configuration["Jwt:Key"];
        }

        public async Task<string> Register(User user)
        {
            // Kiểm tra xem người dùng đã tồn tại chưa
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                throw new Exception("User already exists.");

            // Thêm người dùng mới vào cơ sở dữ liệu
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User registered successfully.";
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
                throw new Exception("Invalid credentials.");

            // Tạo token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }),
                Expires = DateTime.UtcNow.AddDays(1),// Token hết hạn sau 1 ngày
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

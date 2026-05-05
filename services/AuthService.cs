using UserAuthAPI.models;
namespace UserAuthAPI.services
{
    public class AuthService
    {
        private static List<User> users = new List<User>();
        public string Signup(User request)
        {

            bool emailExists = users.Any(u => u.Email == request.Email);
            if (emailExists)
            {
                return ("Email ALready Exist");

            }
            User newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            users.Add(newUser);
            return ("signup successful");
        }



        public object Login(LoginRequest request)


        {
            User existingUser = users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser == null)
            {
                return ("user not found");

            }
            bool PasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password);
            if (!PasswordMatch)
            {
                return ("Wrong Password");

            }
            return new
            {
                existingUser.FirstName,
                existingUser.LastName,

                existingUser.Email


            };


        }


        

    }
}

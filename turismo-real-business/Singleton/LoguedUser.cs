using turismo_real_business.DTOs;

namespace turismo_real_business.Singleton
{
    public static class LoguedUser
    {
        private static UsuarioDTO _loguedUser;
    
        public static void SetLoguedUser(UsuarioDTO loguedUser)
        {
            _loguedUser = loguedUser;
        }

        public static UsuarioDTO GetLoguedUser()
        {
            return _loguedUser;
        }
    }
}

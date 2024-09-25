using LibraryProject.Entities;
using LibraryProject.Interfaces;
using LibraryProject.Data;
using System.Collections.Generic;
using System.Linq;

namespace LibraryProject.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;

        // Inyectamos el DbContext a través del constructor
        public UserService(LibraryContext context)
        {
            _context = context;
        }

        // Crear un nuevo usuario
        public User CreateUser(User user)
        {
            // Validar que el usuario no tenga un ID preexistente (esto es importante si el ID lo genera la base de datos)
            if (user.Id != 0)
            {
                throw new ArgumentException("El ID del usuario debe estar vacío o ser 0 al crear un nuevo usuario.");
            }

            // Agregar el usuario a la base de datos
            _context.Users.Add(user);
            _context.SaveChanges(); // Síncrono
            return user;
        }

        // Obtener un usuario por su ID
        public User? GetUserById(int id)
        {
            // Buscar el usuario por su ID
            var user = _context.Users.Find(id);

            // Si no existe, retornamos null (los controladores manejarán el NotFound)
            return user;
        }

        // Obtener la lista completa de usuarios
        public IEnumerable<User> GetAllUsers()
        {
            // Retorna la lista de usuarios
            return _context.Users.ToList();
        }

        // Actualizar un usuario existente
        public void UpdateUser(User updatedUser)
        {
            // Validar si el usuario existe
            var existingUser = _context.Users.Find(updatedUser.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("El usuario no existe.");
            }

            // Actualizar las propiedades del usuario
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.IsActive = updatedUser.IsActive;

            // Guardar los cambios
            _context.Users.Update(existingUser);
            _context.SaveChanges(); // Síncrono
        }

        // Eliminar un usuario por su ID
        public void DeleteUser(int id)
        {
            // Buscar el usuario a eliminar
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException("El usuario no existe.");
            }

            // Eliminar el usuario
            _context.Users.Remove(user);
            _context.SaveChanges(); // Síncrono
        }
    }
}
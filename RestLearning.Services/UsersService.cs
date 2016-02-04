using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestLearning.Data;
using RestLearning.Data.Models;
using RestLearning.Dtos;

namespace RestLearning.Services {
    public class UsersService {

        public static UserDto GetUser(Guid UserId) {
            using(var uow = new UnitOfWork()) {
                var user = uow.UsersRepository.GetUser(UserId);

                if(user != null) {
                    return MapUsersToDto(user);
                }

                return null;
            }
        }


        public static List<UserDto> GetUsers() {
            using(var uow = new UnitOfWork()) {
                var users = uow.UsersRepository.GetUsers();

                return MapUsersToDto(users);
            }
        }

        public static void CreateUser(UserDto userDto) {
            using(var uow = new UnitOfWork()) {
                var userDe = new Users();
                MapUserToDe(userDto,userDe);
                userDe.AddedOn = DateTime.Now;
                uow.UsersRepository.Insert(userDe);
                uow.Save();
            }
        }

        private static void MapUserToDe(UserDto userDto, Users userDe) {
            userDe.UserID = userDto.UserId;
            userDe.Name = userDto.Name;
            userDe.Age = userDto.Age;
        }

        public static void UpdateUser(UserDto userDto) {
            using(var uow = new UnitOfWork()) {
                var userDe = uow.UsersRepository.GetUser(userDto.UserId);
                userDe.Name = userDto.Name;
                userDe.Age = userDto.Age;
                uow.Save();
            }
        }

        /// <summary>
        /// Return a single user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static UserDto MapUsersToDto(Users user) {
            return new UserDto {
                UserId = user.UserID,
                Age = user.Age,
                Name = user.Name,
                AddedOn = user.AddedOn
            };
        }

        //return a list of users
        private static List<UserDto> MapUsersToDto(List<Users> users) {
            List<UserDto> usersDto = new List<UserDto>();

            foreach(var user in users) {
                usersDto.Add(MapUsersToDto(user));
            }
            return usersDto;
        }
    }
}

﻿using System;
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

        /// <summary>
        /// Return a single user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static UserDto MapUsersToDto(Users user) {
            return new UserDto {
                Age = user.Age,
                Name = user.Name
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

using Core.Entities;
using Data;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Service.Dtos;
using Service.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.Exceptions.ResetException;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository  _groupRepository;

        public GroupService(IGroupRepository appDbContext)
        {
             _groupRepository = appDbContext;
        }
        public int Create(GroupCreateDto createDto)
        {
            if ( _groupRepository.Exists(x => x.No == createDto.No && !x.IsDeleted))
                throw new RestException(StatusCodes.Status409Conflict, "No", "Group alredy exsist");

            //task groupMapper
            //==============================================
            var group = GroupMapper.GroupCreate(createDto);
            //==============================================
             _groupRepository.Add(group);
             _groupRepository.Save();
            return group.Id;
        }

        public List<GroupGetDto> GetAll(string? search = null)
        {
            return  _groupRepository.GetAll(x => x.No.Contains(search)).Select(x => new GroupGetDto
            {
                Id = x.Id,
                No = x.No,
                Limit = x.Limit
            }).ToList();
        }

        public GroupGetDto GetById(int id)
        {
            var data =  _groupRepository.Get(x=>x.Id == id && !x.IsDeleted);

            if(data == null)
            {
                throw new RestException(StatusCodes.Status404NotFound, "No", "not found group");
            }
            //task groupMapper
            //========================================
            return GroupMapper.GroupGetDto(data);
            //========================================
        }

        public void Update(GroupUpdateDto updateDto, int id)
        {
            Group entity = _groupRepository.Get(x => x.Id == id && !x.IsDeleted, "Students");

            if (entity.No != updateDto.No && _groupRepository.Exists(x => x.No == updateDto.No && !x.IsDeleted))
                throw new RestException(StatusCodes.Status400BadRequest, "No", "No already taken");

            if (entity.Students.Count > updateDto.Limit)
                throw new RestException(StatusCodes.Status400BadRequest, "Limit", "There are more students than wanted limit");

            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Group not found");

            entity.No = updateDto.No;
            entity.Limit = updateDto.Limit;
            entity.UpdateAt = DateTime.Now;

            _groupRepository.Save();
        }

        public void Delete(int id)
        {
            Group entity =  _groupRepository.Get(x => x.Id == id && !x.IsDeleted);

            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Group not found");

            //_groupRepository.Delete(entity);
            entity.IsDeleted = true;
            entity.UpdateAt = DateTime.Now;
             _groupRepository.Save();
        }
    }
}

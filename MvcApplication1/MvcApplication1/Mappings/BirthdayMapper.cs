using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Domain.Birthday.Entities;
using MvcApplication1.Models;

namespace MvcApplication1.Mappings
{
    public class BirthdayMapper:Profile
    {
        public BirthdayMapper()
        {
            Mapper.CreateMap<Birthday, BirthdayModels>();
        }
    }
}


using AutoMapper;

using Web.Data.GuestBook.Entities;
using Web.Models.GuestBook;

namespace Web.Data.GuestBook
{
    public class DBMappingProfile
    {
            //cfg.CreateMap<MessageModel,Message>()
            //            .ForMember(target => target.Category, options => options.Ignore());

        MapperConfiguration cfg = new MapperConfiguration(cfg =>
        {


            cfg.CreateMap<Message, MessageDto>().ReverseMap();


            cfg.CreateMap<Category, CategoryDto>().ReverseMap();

        });
       
    }
}


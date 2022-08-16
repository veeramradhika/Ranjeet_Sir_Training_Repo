using AutoMapper;
using BooksAPI.API.API;
using BooksAPI.API.Entities;
using BooksAPI.API.Models;

namespace BooksAPI.API.AutoMapping
{
    public class AutoMapperMapping : Profile
    {
        public AutoMapperMapping()
        {
            CreateMap<BooksModel, ModelApiBook>().ReverseMap();
            CreateMap<AddCart, ModelApiCart>().ReverseMap();
        }
    }
}

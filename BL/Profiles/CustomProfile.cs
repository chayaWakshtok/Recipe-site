using AutoMapper;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiles
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<FeedbackDTO, Feedback>().ReverseMap();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>().ForMember(dest => dest.CountRecipe, opt => opt.MapFrom<CountCategoryRecipeResolver>());
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ForMember(dest => dest.CountLikes, opt => opt.MapFrom<CountUserLikesResolver>())
                .ForMember(dest => dest.CountRecipe, opt => opt.MapFrom<CountUserRecipeResolver>())
                .ForMember(dest=>dest.Picture,opt=>opt.MapFrom< ImageUserResolver>());
            CreateMap<UserDTO,User>();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Recipe, RecipeDTO>().ForMember(dest => dest.CountLikes, opt => opt.MapFrom<CountLikesResolver>())
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageRecipeResolver>());
            CreateMap<Recipe, RecipeLikeDTO>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageLikeRecipeResolver>());
            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Ingredient, IngredientDTO>().ReverseMap();
            CreateMap<Instruction, InstructionDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Likes, LikesDTO>().ReverseMap();
            CreateMap<Follow, FollowDTO>().ReverseMap();
        }

        public class CountLikesResolver : IValueResolver<Recipe, RecipeDTO, int>
        {
            public int Resolve(Recipe source, RecipeDTO destination, int member, ResolutionContext context)
            {
                return source.Likes.Count;
            }
        }

        public class CountUserLikesResolver : IValueResolver<User, UserDTO, int>
        {
            public int Resolve(User source, UserDTO destination, int member, ResolutionContext context)
            {
                return source.Likes.Count;
            }
        }

        public class ImageUserResolver : IValueResolver<User, UserDTO, string>
        {
            public string Resolve(User source, UserDTO destination, string member, ResolutionContext context)
            {
                string myHostUrl = $"https://localhost:7067/Images/";
                return myHostUrl + source.Picture;
            }
        }

        public class ImageRecipeResolver : IValueResolver<Recipe, RecipeDTO, string>
        {
            public string Resolve(Recipe source, RecipeDTO destination, string member, ResolutionContext context)
            {
                string myHostUrl = $"https://localhost:7067/Images/";
                return myHostUrl + source.ImageUrl;
            }
        }

        public class ImageLikeRecipeResolver : IValueResolver<Recipe, RecipeLikeDTO, string>
        {
            public string Resolve(Recipe source, RecipeLikeDTO destination, string member, ResolutionContext context)
            {
                string myHostUrl = $"https://localhost:7067/Images/";
                return myHostUrl + source.ImageUrl;
            }
        }

        public class CountUserRecipeResolver : IValueResolver<User, UserDTO, int>
        {
            public int Resolve(User source, UserDTO destination, int member, ResolutionContext context)
            {
                return source.Recipes.Count;
            }
        }

        public class CountCategoryRecipeResolver : IValueResolver<Category, CategoryDTO, int>
        {
            public int Resolve(Category source, CategoryDTO destination, int member, ResolutionContext context)
            {
                return source.Recipes.Count;
            }
        }

    }
}

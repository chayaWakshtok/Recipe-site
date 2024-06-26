USE [master]
GO
/****** Object:  Database [RecipesSite]    Script Date: 09/06/2024 22:24:45 ******/
CREATE DATABASE [RecipesSite]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RecipesSite', FILENAME = N'C:\Users\User\RecipesSite.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RecipesSite_log', FILENAME = N'C:\Users\User\RecipesSite_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [RecipesSite] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RecipesSite].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RecipesSite] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RecipesSite] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RecipesSite] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RecipesSite] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RecipesSite] SET ARITHABORT OFF 
GO
ALTER DATABASE [RecipesSite] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RecipesSite] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RecipesSite] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RecipesSite] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RecipesSite] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RecipesSite] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RecipesSite] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RecipesSite] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RecipesSite] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RecipesSite] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RecipesSite] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RecipesSite] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RecipesSite] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RecipesSite] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RecipesSite] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RecipesSite] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RecipesSite] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RecipesSite] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RecipesSite] SET  MULTI_USER 
GO
ALTER DATABASE [RecipesSite] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RecipesSite] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RecipesSite] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RecipesSite] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RecipesSite] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RecipesSite] SET QUERY_STORE = OFF
GO
USE [RecipesSite]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [RecipesSite]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[Image] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateAt] [datetime] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Difficulties]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Difficulties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Difficulties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mark] [nvarchar](50) NULL,
	[UserId] [int] NULL,
	[Type] [int] NULL,
	[RecipeId] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateAt] [datetime] NULL,
	[FeedbackId] [int] NULL,
 CONSTRAINT [PK_feedbacks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Follows]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Follows](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToUser] [int] NULL,
	[FromUser] [int] NULL,
 CONSTRAINT [PK_Follows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Count] [decimal](18, 2) NULL,
	[TypeCount] [int] NULL,
	[RecipeId] [int] NULL,
	[ProductId] [int] NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructions]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Step] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[RecipeId] [int] NULL,
 CONSTRAINT [PK_Instructions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Likes]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Likes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[RecipeId] [int] NULL,
 CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[VideoUrl] [nvarchar](200) NULL,
	[Servings] [float] NULL,
	[PrepTime] [float] NULL,
	[Calories] [float] NULL,
	[Fat] [float] NULL,
	[Protein] [float] NULL,
	[Carbs] [float] NULL,
	[UserId] [int] NULL,
	[DifficultyId] [int] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[UpdateAt] [datetime] NULL,
	[CategoryId] [int] NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/06/2024 22:24:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdateAt] [datetime] NULL,
	[Email] [nvarchar](50) NULL,
	[PasswordHash] [varbinary](1024) NULL,
	[Picture] [nvarchar](max) NULL,
	[RoleId] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[PasswordSalt] [varbinary](1024) NULL,
	[AboutMe] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [Status], [CreateAt], [UpdateAt]) VALUES (3, N'pizza', N'pizza', N'category_2f92111b-6b20-4df2-bfff-d779343143f8.jpg', 1, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [Status], [CreateAt], [UpdateAt]) VALUES (4, N'Pasta', N'Pasta', N'category_5de3b824-75b7-4283-8b7c-25c0a206f442.jpg', 1, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [Status], [CreateAt], [UpdateAt]) VALUES (1002, N'cookies', N'cookies', N'category_7968c584-b844-479d-8b4e-43d66514a9b3.jpg', 1, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description], [Image], [Status], [CreateAt], [UpdateAt]) VALUES (1003, N'soup', N'soup', N'category_e3766bfb-72f0-46d6-b288-207daa59f3b8.jpg', 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Difficulties] ON 

INSERT [dbo].[Difficulties] ([Id], [Name], [Status]) VALUES (1, N'very hard', 1)
INSERT [dbo].[Difficulties] ([Id], [Name], [Status]) VALUES (2, N'hard', 1)
INSERT [dbo].[Difficulties] ([Id], [Name], [Status]) VALUES (3, N'easy', 1)
SET IDENTITY_INSERT [dbo].[Difficulties] OFF
GO
SET IDENTITY_INSERT [dbo].[Follows] ON 

INSERT [dbo].[Follows] ([Id], [ToUser], [FromUser]) VALUES (1, 2002, 4003)
INSERT [dbo].[Follows] ([Id], [ToUser], [FromUser]) VALUES (2, 4003, 2004)
INSERT [dbo].[Follows] ([Id], [ToUser], [FromUser]) VALUES (3, 4003, 3004)
INSERT [dbo].[Follows] ([Id], [ToUser], [FromUser]) VALUES (4, 3004, 4003)
SET IDENTITY_INSERT [dbo].[Follows] OFF
GO
SET IDENTITY_INSERT [dbo].[Ingredients] ON 

INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (1, CAST(0.75 AS Decimal(18, 2)), 11, 6, 2)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (2, CAST(1.00 AS Decimal(18, 2)), 11, 6, 3)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (3, CAST(2.00 AS Decimal(18, 2)), 4, 6, 1)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (4, CAST(4.00 AS Decimal(18, 2)), 11, 6, 4)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (5, CAST(0.75 AS Decimal(18, 2)), 11, 7, 2)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (6, CAST(1.00 AS Decimal(18, 2)), 11, 7, 3)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (7, CAST(2.00 AS Decimal(18, 2)), 4, 7, 1)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (8, CAST(4.00 AS Decimal(18, 2)), 11, 7, 4)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (9, CAST(2.00 AS Decimal(18, 2)), 12, 8, 5)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (10, CAST(1.00 AS Decimal(18, 2)), 12, 8, 6)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (11, CAST(1.00 AS Decimal(18, 2)), 3, 8, 7)
INSERT [dbo].[Ingredients] ([Id], [Count], [TypeCount], [RecipeId], [ProductId]) VALUES (12, CAST(6.00 AS Decimal(18, 2)), 13, 8, 8)
SET IDENTITY_INSERT [dbo].[Ingredients] OFF
GO
SET IDENTITY_INSERT [dbo].[Instructions] ON 

INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (1, 1, N'Preheat oven to 350°F.', 6)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (2, 2, N'In the bowl of a mixer, cream together oil and sugar. Add eggs and continue to mix until creamy.', 6)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (3, 3, N'Add flour, baking powder, salt, orange juice, orange zest, lemon juice, and vanilla. Mix until combined.', 6)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (4, 4, N'Roll out dough and shape into cookies/hamantaschen. Place on parchment-lined baking sheet and bake for 15 minutes.', 6)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (5, 1, N'Preheat oven to 350°F.', 7)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (6, 2, N'In the bowl of a mixer, cream together oil and sugar. Add eggs and continue to mix until creamy.', 7)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (7, 3, N'Add flour, baking powder, salt, orange juice, orange zest, lemon juice, and vanilla. Mix until combined.', 7)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (8, 4, N'Roll out dough and shape into cookies/hamantaschen. Place on parchment-lined baking sheet and bake for 15 minutes.', 7)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (9, 1, N'In a 6-quart pot add turkey bones and turkey thighs. Add cold water to cover bones by an inch.', 8)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (10, 2, N'Add garlic and salt. Bring liquid to a boil. Reduce to a simmer. Skim any scum (white/brownish foam) that forms on the top of the pot with a slotted spoon or ladle. Allow broth to simmer for two hours until liquid is reduced to a third.', 8)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (11, 3, N'While broth is simmering, heat a frying pan. Add oil and swirl around the pan until the bottom of the pan is coated in oil. Add diced onions, and don’t stir to allow onions to buildup color (when you mix the pan too much you invite unwanted cold air into your pan which in turn ends up sweating your vegetables as opposed to sautéing). Allow onions to sauté for 2 minutes before stirring. Once the edges of the diced onions develop brown edges (super important for your onions to develop color, its a part of the flavor building for the soup), lower heat and allow onions to cook until translucent. Set aside.', 8)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (12, 4, N'Remove bones and thighs from the broth. Toss bones and set turkey thighs aside if you’d like to shred it up and add back to the soup (I personally find the meat more on the dry side, so I usually toss it).', 8)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (13, 5, N'Add chopped zucchini and squash to the pot along with half the sauteed onions. Cover soup with a lid, and allow soup to cook for 30 minutes, or until squash is tender but not overcooked (overcooked squash turns black, avoid that as it doesn’t make for a very appealing color).', 8)
INSERT [dbo].[Instructions] ([Id], [Step], [Description], [RecipeId]) VALUES (14, 6, N'Just before pureeing the soup add the remaining sauteed onions, and nutritional yeast if you choose (I love to add it, it adds a mock cheesy flavor that I find irresistible). Using a hand blender/immersion blender, puree the soup until all the vegetables are broken down and mostly smooth. Aim for a mostly smooth texture but not baby food status (if you over blend you will lose color and texture). Add black pepper. Taste for seasoning, add more salt, or pepper if necessary.', 8)
SET IDENTITY_INSERT [dbo].[Instructions] OFF
GO
SET IDENTITY_INSERT [dbo].[Likes] ON 

INSERT [dbo].[Likes] ([Id], [UserId], [RecipeId]) VALUES (1, 4003, 7)
SET IDENTITY_INSERT [dbo].[Likes] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name]) VALUES (1, N'egg')
INSERT [dbo].[Products] ([Id], [Name]) VALUES (2, N'oil')
INSERT [dbo].[Products] ([Id], [Name]) VALUES (3, N'sugar')
INSERT [dbo].[Products] ([Id], [Name]) VALUES (4, N'flour')
INSERT [dbo].[Products] ([Id], [Name]) VALUES (5, N'nutritional yeast')
INSERT [dbo].[Products] ([Id], [Name]) VALUES (6, N'salt')
INSERT [dbo].[Products] ([Id], [Name]) VALUES (7, N' Vidalia or Spanish onion')
INSERT [dbo].[Products] ([Id], [Name]) VALUES (8, N'fresh garlic cloves')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Recipes] ON 

INSERT [dbo].[Recipes] ([Id], [Title], [Description], [VideoUrl], [Servings], [PrepTime], [Calories], [Fat], [Protein], [Carbs], [UserId], [DifficultyId], [Status], [CreateAt], [UpdateAt], [CategoryId], [ImageUrl]) VALUES (6, N'No-Margarine Sugar Cookie', N'Prefer not using margarine? Or, want to bake and you simply don’t have it on hand? This is a great alternative. It’s a great dough to use for hamantaschen too!
There’s lots of reasons why everyone would want an oil-based cookie or hamantaschen. One, it’s simply more convenient. You always have oil in the house, but you might need to go out and buy margarine when you’re planning to make a specific recipe. And, sometimes we just want to bake on a whim! Two, many people like to avoid margarine and opt for alternatives that are a bit healthier. And that includes Estee Kafra of Pure Foods by Estee, who recommended that we try this recipe when using her new sprinkles!

Yes, this one does need a mixer but it comes together quickly.', NULL, 40, 20, NULL, NULL, NULL, 1000, 2003, 3, 1, CAST(N'2024-03-27T22:57:04.503' AS DateTime), CAST(N'2024-03-27T22:57:04.650' AS DateTime), 1002, N'recipe_bfbcdb6b-92b2-408d-a9ff-2f97a5178ebe.jpg')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [VideoUrl], [Servings], [PrepTime], [Calories], [Fat], [Protein], [Carbs], [UserId], [DifficultyId], [Status], [CreateAt], [UpdateAt], [CategoryId], [ImageUrl]) VALUES (7, N'No-Margarine Sugar Cookie', N'Prefer not using margarine? Or, want to bake and you simply don’t have it on hand? This is a great alternative. It’s a great dough to use for hamantaschen too!
There’s lots of reasons why everyone would want an oil-based cookie or hamantaschen. One, it’s simply more convenient. You always have oil in the house, but you might need to go out and buy margarine when you’re planning to make a specific recipe. And, sometimes we just want to bake on a whim! Two, many people like to avoid margarine and opt for alternatives that are a bit healthier. And that includes Estee Kafra of Pure Foods by Estee, who recommended that we try this recipe when using her new sprinkles!

Yes, this one does need a mixer but it comes together quickly.', NULL, 40, 20, NULL, NULL, NULL, 1000, 2003, 3, 1, CAST(N'2024-03-27T23:02:31.247' AS DateTime), CAST(N'2024-03-27T23:02:31.247' AS DateTime), 1002, N'recipe_910a219a-6310-47b6-8425-8b8bdca31f64.jpg')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [VideoUrl], [Servings], [PrepTime], [Calories], [Fat], [Protein], [Carbs], [UserId], [DifficultyId], [Status], [CreateAt], [UpdateAt], [CategoryId], [ImageUrl]) VALUES (8, N'A Super Flavorful Zucchini Soup with Turkey Broth', N'No more bland zucchini soup. Turkey bones is the secret to a richly flavorful soup. 
Zucchini soups have a long reputation for being bland when they’re not being helped by artificial flavorings. Since zucchini is a vegetable with subtle flavor, it could really use help to shine. This time leave your onion soup mix in the pantry and turn to turkey bones as your answer to flavor. 
Turkey bones are a step up in flavor from chicken bones. They have a richer depth of flavor than chicken bones without being overpowering like beef bones can sometimes be. Turning turkey bones into turkey stock in 2 hours is all the flavor you need for this soup. ', NULL, 6, 20, NULL, NULL, NULL, 6, 2003, 2, 1, CAST(N'2024-03-27T23:21:26.937' AS DateTime), CAST(N'2024-03-27T23:21:26.940' AS DateTime), 1003, N'recipe_8e19d33f-8d56-4604-939c-bc00743e428d.jpg')
SET IDENTITY_INSERT [dbo].[Recipes] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (2002, N'efratyi1', 1, CAST(N'2023-10-19T22:10:13.123' AS DateTime), CAST(N'2023-11-30T21:05:38.067' AS DateTime), N'RI7@GMAIL.COM', 0x310032003300340035003600370038003900, N'https://localhost:7067/Images/https://localhost:7067/Images/https://localhost:7067/Images/female_defualt.png', 2, N'efrat', NULL, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (2003, N'chayaw', 1, CAST(N'2023-11-18T21:04:41.777' AS DateTime), CAST(N'2023-11-18T21:04:42.277' AS DateTime), N'c@gmail.com', 0xB5C69BAFEBA58D5D20C3507D00F27E2365810E77584082444C67A19A8026B496D69F0F6FB17EAEA518F1D281FE5B08045DB37DAF25743EB8799D591DBDB910B4, N'female_defualt.png', 1, N'chaya', 0x6BBA315A3085D437A1BCA5409D26FA000BCE04B367D896F26A78A75A1B4802BEEEC65DA100348C982C5FE67B9047805F1E63510C96DC9EF735EAA3935AB35CAE2BEFE122B5ECBC2721F0E8E0D4E7829AB73161DDB1BCDF64CB77003AF9B9C3FF129194404926852BB685EC221CC1BE69609690EBD31D6F7ECE53CAEE8B222A21, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (2004, N'shir', 1, CAST(N'2023-11-18T21:07:11.923' AS DateTime), CAST(N'2023-11-18T21:07:11.923' AS DateTime), N'shir@gmail.com', 0xAD3E48739D19BC584EBAA4A3B6051E3D309B11F954F1C47FB1BC50D27B5D7BD3551DE6ED8276B7B3418AFC3C295C646F25FF013B71D6B7AD24A02A6C9C568889, N'female_defualt.png', 2, N'shir', 0xEBBEF699E852C86D60EC43A7A63B2E4A92DDFBD82E6BB151841E251F23D0312E6E484541D1B4E909D067D72A8E9101691EDE41656915A436C7C8F4BEC4A38C2423CCEE5B5803C5AB6CDAD681DB6091CC800770AA4CEB399138F06A53754A7A40E0AD3EA559A277F51A4EC7416FCF18AC363BA7517D885F0878D3E45C145B9613, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (3004, N'tamari', 1, CAST(N'2023-11-30T21:13:22.227' AS DateTime), CAST(N'2023-11-30T21:13:22.227' AS DateTime), N'RI775969@GMAIL.COM', 0x59BE0966D903CA965F0758D1806716B5F7C686C4C9003F1CFCB69A673EBB520260798C23D84C23C7883CF9D56F1545625FE0A5311BB6B7479CC5855F5026F7EE, N'female_defualt.png', 2, N'tamar', 0x95189D93AE64D76BF4A86CAB003B8FBA69543EE899BD9D029E5B8BA1FC1239F33F5453BC1F0ABC6375A19AE8DB617CD9C5048180B73FAB0432B4358FA22F6F170375BEE856F0CFFA9048665731DAA43CB4369D4B175A23D710283F69C5EACB8F5E173FC9E127E7D6225263B11D9CA0ADAC571BD363E749292E4B129E56879BC9, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (4003, N'gavriel', 1, CAST(N'2024-05-27T21:40:32.737' AS DateTime), CAST(N'2024-05-30T23:43:59.593' AS DateTime), N'RI22@GMAIL.COM', 0xC4F167C1CFF5AAE6DC12CD23D4496893DD7247559D99F5EB19213B5A63E0265D09A38C96721343894C89F3EC59ED269F7D9863B729DA88F9D2056BEFF6034FD9, N'_48ac9533-0312-4f9d-ad9f-c7e65d4cd2e0.jpg', 2, N'gavriel', 0x795C49A91F07923B55C2118D4CA640FF18F800454D909C81F9076246366D9F2148D529854B4F1B32FCEA520DF0456E3853016A8FDA31035D6155CAB408CEC7E6E5C4B6C70055229419A0B7F629F2088D9BE06AAC813C10FF36533C2A5A020D82F4547C795A463AA234F9260CF879EEFF475948A92D1608CB39D55D283EFCEBE4, N'only me good')
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (5003, N'7aa', 1, CAST(N'2024-06-04T21:25:34.440' AS DateTime), CAST(N'2024-06-04T21:25:34.440' AS DateTime), N'c05@gmail.com', 0x6C570C076E50C2139B403858348FABDDED8241F5B2938D2A1C3A39A8E9FEE34FF9D114C9CB84362EBDFFE9E571D1E91E3FCA05DC7AD277F36203889BAC8B34C2, N'male_default.jpg', 2, N'7aa', 0x86E3D322489B5FD98F489FCEAF6FF567DE37270361D4935E9486FA93DE27309B3E7E4FD73D0B7FCA319A2CC90008C5019D61FC1247623D2A289BF10E201C0B578241AF86E670887B9F35BE46DA5495B77D72DD984EA97402F62AA5A99A429386BE3145C3CA43EE4CBB8F785CB0C9461D452173D9431F2B97DC053EFBF6395109, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (5004, N'ccc', 1, CAST(N'2024-06-04T21:26:22.470' AS DateTime), CAST(N'2024-06-04T21:26:22.470' AS DateTime), N'c@gmail.com', 0x40B7DDEF03E48DBF2497E67798589BDA6C22B2463E498F1135F2A8867EBB516ADCC4D89DC1FD1346299F6CB53F09DF10D3B0F68779E8583700DA45C0BFD90BB1, N'male_default.jpg', 2, N'ccc', 0x85DA47B27DC747B4C5B84C7D3615E6CDFE86D899CBEB5057B4332AD48C05B1CB481623F60F91969A81A307FE59497148EF4540F0C32E591EE80C394C22953988C8A8C57A1C520A075E35912F2A4BA6B5336F9569A317C88EC1939CA780216D84A3E5E68919E55F4B3BEAA45B65A893259EFF9BF329CE9EC07184DE7CE09AC90B, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (5005, N'daniel', 1, CAST(N'2024-06-04T21:28:07.253' AS DateTime), CAST(N'2024-06-04T21:28:07.253' AS DateTime), N'c0556@gmail.com', 0x2CD9CCD743F165BB7AA8764DED45573BE2564856BD921955386BE222FEC09A8A6B5FED2AA7D12436856A01C822A38793EECD4CD1D9334E4C1E2CC131173A5A8E, N'male_default.jpg', 2, N'daniel', 0x8862E630D288920A884EFE0DF2D00183D4E038A0449B602D811DE5035C2833AD5954E8DBBECEDF2751981DA52CA62A98B66281D5778E5CD7EB0758F245EA870BF6913C2BC25B987C5B07D768BE5AD34278A6C0593AC6D532DA3D73BA0DFE16746E7DD096FA3A76A9135941786278ED373BF08A5570C636F864C8F86C95EA4A0E, NULL)
INSERT [dbo].[Users] ([Id], [Username], [Status], [CreateAt], [UpdateAt], [Email], [PasswordHash], [Picture], [RoleId], [FirstName], [PasswordSalt], [AboutMe]) VALUES (5006, N'rachel', 1, CAST(N'2024-06-04T21:28:54.233' AS DateTime), CAST(N'2024-06-04T21:28:54.233' AS DateTime), N'RI775@GMAIL.COM', 0xB30259D8D7EA0BC0C3D832C99052E03C2A8E8019D548D33722C10336E2D38E20A88933803723CE10BF6CB6DE0F9E290C14606C26F7B2A32029624C5C86EA9D4A, N'female_defualt.png', 2, N'rachel', 0xF39DC0E111A6FF5BA4892C9AC5D44E303AC927397C30FFB38725FF190D369059E23A9A1DF14851BB53667022F3C62D303488D230EF35C78FFB1718B921A44FF5EA51FF1CDC88806A8853A3E6176E1F4CCAAC415918ABE3FE272193D37CA78F6CDFB42E5F29A7B68D2332FEAF28AA586DF066833BD3F68C71AFDCB800DAA3B9DA, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_Recipes] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([Id])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_Recipes]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_Users]
GO
ALTER TABLE [dbo].[Follows]  WITH CHECK ADD  CONSTRAINT [FK_Follows_Users] FOREIGN KEY([FromUser])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Follows] CHECK CONSTRAINT [FK_Follows_Users]
GO
ALTER TABLE [dbo].[Follows]  WITH CHECK ADD  CONSTRAINT [FK_Follows_Users1] FOREIGN KEY([ToUser])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Follows] CHECK CONSTRAINT [FK_Follows_Users1]
GO
ALTER TABLE [dbo].[Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Ingredients_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Ingredients] CHECK CONSTRAINT [FK_Ingredients_Products]
GO
ALTER TABLE [dbo].[Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Ingredients_Recipes] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([Id])
GO
ALTER TABLE [dbo].[Ingredients] CHECK CONSTRAINT [FK_Ingredients_Recipes]
GO
ALTER TABLE [dbo].[Instructions]  WITH CHECK ADD  CONSTRAINT [FK_Instructions_Recipes] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([Id])
GO
ALTER TABLE [dbo].[Instructions] CHECK CONSTRAINT [FK_Instructions_Recipes]
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD  CONSTRAINT [FK_Likes_Recipes] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([Id])
GO
ALTER TABLE [dbo].[Likes] CHECK CONSTRAINT [FK_Likes_Recipes]
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD  CONSTRAINT [FK_Likes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Likes] CHECK CONSTRAINT [FK_Likes_Users]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Categories]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Difficulties] FOREIGN KEY([DifficultyId])
REFERENCES [dbo].[Difficulties] ([Id])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Difficulties]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [RecipesSite] SET  READ_WRITE 
GO

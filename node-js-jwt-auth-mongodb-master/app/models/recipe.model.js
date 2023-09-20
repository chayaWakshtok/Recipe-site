const mongoose = require("mongoose");

const Recipe = mongoose.model(
  "Recipe",
  new mongoose.Schema({
    title: String,
    description: String,
    image: String,
    servings: Number,
    prepTime: Number,
    calories: Number,
    images: [String],
    user:
    {
      type: mongoose.Schema.Types.ObjectId,
      ref: "User"
    },
    likes: Number,
    difficulty:
    {
      type: mongoose.Schema.Types.ObjectId,
      ref: "Difficulty"
    },
    products:
      [{
        type: mongoose.Schema.Types.ObjectId,
        ref: "Product"
      }],
    feedbacks:
      [{
        type: mongoose.Schema.Types.ObjectId,
        ref: "Feedback"
      }],
    status: Number,
  },
    { timestamps: true })
);

module.exports = Recipe;

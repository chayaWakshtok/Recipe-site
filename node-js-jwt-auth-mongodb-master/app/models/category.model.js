const mongoose = require("mongoose");

const Category = mongoose.model(
  "Category",
  new mongoose.Schema({
    name: String,
    description: String,
    image: String,
    status: Number,
    recipes: [{
      type: mongoose.Schema.Types.ObjectId,
      ref: "Recipe"
    } ]
  },
    { timestamps: true })
);

module.exports = Category;

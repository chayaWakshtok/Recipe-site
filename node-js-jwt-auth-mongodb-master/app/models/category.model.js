const mongoose = require("mongoose");

const Category = mongoose.model(
  "Category",
  new mongoose.Schema({
    name: String,
    description: String,
    image: String,
    status:Number,
  },
  { timestamps: true })
);

module.exports = Category;

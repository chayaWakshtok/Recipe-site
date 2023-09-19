const mongoose = require("mongoose");

const Product = mongoose.model(
  "Product",
  new mongoose.Schema({
    name: String,
    count: Number,
  },
  { timestamps: true })
);

module.exports = Product;

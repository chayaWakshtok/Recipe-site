const mongoose = require("mongoose");

const Difficulty = mongoose.model(
  "Difficulty",
  new mongoose.Schema({
    name: String,
    status:Number,
  })
);

module.exports = Difficulty;

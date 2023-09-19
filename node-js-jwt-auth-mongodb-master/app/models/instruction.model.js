const mongoose = require("mongoose");

const Instruction = mongoose.model(
  "Instruction",
  new mongoose.Schema({
    step: Number,
    description: String,
  },
  { timestamps: true })
);

module.exports = Instruction;

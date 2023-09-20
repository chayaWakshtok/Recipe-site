const mongoose = require("mongoose");

const Feedback = mongoose.model(
  "Feedback",
  new mongoose.Schema({
    mark: String,
    user: {
      type: mongoose.Schema.Types.ObjectId,
      ref: "User"
    },
    recipe: {
      type: mongoose.Schema.Types.ObjectId,
      ref: "Recipe"
    }
  },
    { timestamps: true })
);

module.exports = Feedback;

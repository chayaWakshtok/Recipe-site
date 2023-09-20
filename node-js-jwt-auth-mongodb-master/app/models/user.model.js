const mongoose = require("mongoose");

const User = mongoose.model(
  "User",
  new mongoose.Schema({
    username: String,
    email: String,
    password: String,
    status: Number,
    picture: String,
    role:
    {
      type: mongoose.Schema.Types.ObjectId,
      ref: "Role"
    }
  },
    { timestamps: true })
);

module.exports = User;

const mongoose = require('mongoose');
mongoose.Promise = global.Promise;

const db = {};

db.mongoose = mongoose;
db.difficulty=require("./difficulty.model");
db.user = require("./user.model");
db.role = require("./role.model");
db.category=require("./category.model");
db.product=require("./product.model");

db.ROLES = ["user", "admin"];

module.exports = db;
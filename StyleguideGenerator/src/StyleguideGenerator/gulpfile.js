/// <binding BeforeBuild='build' ProjectOpened='sass:watch' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
var dir = (function () {
    var from = {};
    from.root = "./Src";
    from.css = from.root + "/Styles";
    from.scripts = from.root + "/Scripts";
    from.sass = from.css + "/sass";
    var to = {};
    to.root = "./wwwroot";
    to.css = to.root + "/css";
    to.scripts = to.root + "/scripts";
    var jquery = "./wwwroot/lib/jquery/dist/jquery.min.js";
    return { from: from, to: to, jquery: jquery };
})();

var gulp = require('gulp');
var plug = {
    sass : require("gulp-sass"),
    csso : require("gulp-csso"),
    rename : require("gulp-rename"),
    jsmin: require('gulp-jsmin'),
    jsmin2: require("gulp-minify")
}


gulp.task("cssparse", function () {
    return gulp.src([dir.from.scripts + "/*.js"])
        .pipe(plug.jsmin2({
            ext: {
                src: '.debug.js',
                min: '.min.js'
            },
            noSource: false
        }))
      .pipe(gulp.dest(dir.to.scripts));
});


gulp.task("cssmin", function () {
    gulp.src([dir.from.sass + "/*.scss"])
      .pipe(plug.sass().on("error", plug.sass.logError))
      .pipe(plug.csso({
          restructure: false,
          sourceMap: false,
          debug: false
      }))
      .pipe(plug.rename({
          suffix: ".min"
      }))
      .pipe(gulp.dest(dir.to.css));
});

gulp.task("jsmin", function () {
    return gulp.src([dir.from.scripts + "/*.js"])
        .pipe(plug.jsmin2({
            ext: {
                src: '.debug.js',
                min: '.min.js'
            },
            noSource: false
        }))
      .pipe(gulp.dest(dir.to.scripts));
});

gulp.task("sass", function () {
    gulp.src([dir.from.sass + "/*.scss"])
      .pipe(plug.sass().on("error", plug.sass.logError))
      .pipe(gulp.dest(dir.from.css));
});
gulp.task("sass:watch", function () {
    gulp.watch(dir.from.sass + "/**", ["sass"]);
});

gulp.task("jquery", function () {
    return gulp.src(dir.jquery)
        .pipe(gulp.dest(dir.to.scripts));
});

gulp.task("build", function() {
    gulp.run("jquery");
    gulp.run("sass");
    gulp.run("jsmin");
});

gulp.task("default", function () {
    gulp.run("jquery");
    //gulp.watch(dir.from.sass+"/**", ["sass"]);
    return null;
});

//var sass = require("gulp-sass");
//var csso = require("gulp-csso");
//var rename = require("gulp-rename");
//var jsmin = require('gulp-jsmin');


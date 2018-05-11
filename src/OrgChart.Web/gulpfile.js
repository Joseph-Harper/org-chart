/// <binding AfterBuild='default' />
var gulp = require("gulp");
var uglify = require("gulp-uglify");
var concat = require("gulp-concat");

gulp.task('minify', function () {
    return gulp.src('wwwroot/js/**/*.js')
        .pipe(uglify())
        .pipe(concat('site.min.js'))
        .pipe(gulp.dest('wwwroot/dist'));
});

gulp.task('copy-bootstrap-css', function () {
    return gulp.src('node_modules/bootstrap/dist/css/**')
        .pipe(gulp.dest('wwwroot/lib/bootstrap/dist/css'));
});

gulp.task('copy-bootstrap-js', function () {
    return gulp.src('node_modules/bootstrap/dist/js/**')
        .pipe(gulp.dest('wwwroot/lib/bootstrap/dist/js'));
});

gulp.task('default', []);

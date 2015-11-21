var gulp = require('gulp');
var uglify = require('gulp-uglify');
var shell = require('gulp-shell');
var plumber = require('gulp-plumber');
var livereload = require('gulp-livereload');

gulp.task('scripts', function() {
    gulp.src('src/main/webapp/resources/js/*.js')
        .pipe(plumber())
        .pipe(uglify())
        .pipe(gulp.dest('src/main/webapp/resources/js/build'))
        //.pipe(livereload({ start: true }));
});

gulp.task('java', shell.task([
    'gradle deployB2'
]));

gulp.task('watch', function() {
    gulp.watch('src/main/**/*.*', ['scripts', 'java']);
});

gulp.task('default', ['scripts', 'java', 'watch']);

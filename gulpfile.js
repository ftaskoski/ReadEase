const gulp = require('gulp');
const dotnet = require('gulp-dotnet-cli');

gulp.task('build-dotnet', () => {
    return gulp.src('./')
        .pipe(dotnet.build());
});
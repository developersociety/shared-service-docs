let mix = require('laravel-mix')
mix.setResourceRoot('/profiles/contrib/resourcehub-distribution/themes/resourcehub_theme/dist')
mix.setPublicPath('dist');
mix
  .sass('scss/styles.scss', 'css')
  .sass('scss/colors.scss', 'css')
  .browserSync({
    port: 35729,
    proxy: 'appserver',
    files: [
      'dist/css/**/*',
      'templates/**/*',
    ],
  })
  .webpackConfig({
    module: {
      rules: [
        {
          test: /\.scss/,
          loader: 'import-glob-loader'
        }
      ]
    }
  })

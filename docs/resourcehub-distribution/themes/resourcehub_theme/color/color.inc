<?php
// phpcs:ignoreFile

/**
 * @file
 * Lists available colors and color schemes for the Bartik theme.
 */

$info = [
  // Available colors and color labels used in theme.
  'fields' => [
    'accent' => t('Accent'),
  ],
  // Pre-defined color schemes.
  'schemes' => [
    'default' => [
      'title' => t('Resource Hub default'),
      'colors' => [
        'accent' => '#efefef',
      ],
    ],
  ],
  // CSS files (excluding @import) to rewrite with new color scheme.
  'css' => [
    'dist/css/colors.css',
  ],
  // Files to copy.
  'copy' => [
    'logo.svg',
  ],
];

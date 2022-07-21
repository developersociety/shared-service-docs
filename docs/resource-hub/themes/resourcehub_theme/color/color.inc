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
    'accent2' => t('Accent 2'),
    'header' => t('Header/Footer background'),
    'headertext' => t('Header/footer text'),
    'mobile_nav_bg' => t('Mobile navigation background'),
    'mobile_nav_link' => t('Mobile navigation link'),
  ],
  // Pre-defined color schemes.
  'schemes' => [
    'default' => [
      'title' => t('Resource Hub default'),
      'colors' => [
        'accent' => '#333332',
        'accent2' => '#f35c49',
        'header' => '#ebeae9',
        'headertext' => '#0b132c',
        'mobile_nav_bg' => '#333333',
        'mobile_nav_link' => '#ffffd9',
      ],
    ],
    'red' => [
      'title' => t('Red'),
      'colors' => [
        'accent' => '#f44336',
        'accent2' => '#ffcdd2',
        'header' => '#000000',
        'headertext' => '#ffffff',
        'mobile_nav_bg' => '#333333',
        'mobile_nav_link' => '#ffffd9',
      ],
    ],
    'ac' => [
      'title' => t('Agile Collective'),
      'colors' => [
        'accent' => '#f44336',
        'accent2' => '#23334b',
        'header' => '#ffffff',
        'headertext' => '#23334b',
        'mobile_nav_bg' => '#333333',
        'mobile_nav_link' => '#ffffd9',
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

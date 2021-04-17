<?php

/**
 * @file
 * Theme settings form for Resource Hub Default theme.
 */

/**
 * Implements hook_form_system_theme_settings_alter().
 */
function resourcehub_theme_form_system_theme_settings_alter(&$form, &$form_state) {

  $form['resourcehub_theme'] = [
    '#type' => 'details',
    '#title' => t('Resource Hub Default'),
    '#open' => TRUE,
  ];

  $form['resourcehub_theme']['font_size'] = [
    '#type' => 'number',
    '#title' => t('Font size'),
    '#min' => 12,
    '#max' => 18,
    '#default_value' => theme_get_setting('font_size'),
  ];

}

<?php

/**
 * @file
 * Theme settings form for Resource Hub Default theme.
 */

/**
 * Implements hook_form_system_theme_settings_alter().
 */
function resourcehub_theme_form_system_theme_settings_alter(&$form, &$form_state) {

  unset($form['theme_settings']['toggle_node_user_picture']);
  unset($form['theme_settings']['toggle_comment_user_picture']);
  unset($form['theme_settings']['toggle_comment_user_verification']);

  $form['theme_settings']['toggle_branding_logo'] = [
    '#type' => 'checkbox',
    '#title' => 'Site logo',
    '#default_value' => theme_get_setting('features.branding_logo', 'resourcehub_theme'),
  ];
  $form['theme_settings']['toggle_branding_name'] = [
    '#type' => 'checkbox',
    '#title' => 'Site name',
    '#default_value' => theme_get_setting('features.branding_name', 'resourcehub_theme'),
  ];
  $form['logo']['#states'] = [
    // Hide the logo settings fieldset when shortcut icon display
    // is disabled.
    'invisible' => [
      'input[name="toggle_branding_logo"]' => ['checked' => FALSE],
    ],
  ];
  $form['#attached']['html_head'][] = [
    [
      '#tag' => 'style',
      '#value' => '.color-preview, .color-form h2 { display:none!important; }',
    ],
    'resourcehub_theme_css',
  ];
}

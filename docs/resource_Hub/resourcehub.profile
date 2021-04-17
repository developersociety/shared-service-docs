<?php

/**
 * @file
 * Enables modules and site configuration for a resource hub site installation.
 */

use Drupal\Core\Form\FormStateInterface;
use Drupal\taxonomy\Entity\Term;

/**
 * Implements hook_theme().
 */
function resourcehub_theme($existing, $type, $theme, $path) {
  return [
    'resourcehub_search' => [
      'variables' => [],
    ],
  ];
}

/**
 * Implements hook_form_FORM_ID_alter() for install_configure_form().
 *
 * Allows the profile to alter the site configuration form.
 */
function resourcehub_form_install_configure_form_alter(&$form, FormStateInterface $form_state) {
  $form['install_demo'] = [
    '#type' => 'checkbox',
    '#title' => 'Install demo content',
  ];
  $form['#submit'][] = 'resourcehub_form_install_configure_submit';
}

/**
 * Submission handler to enable the demo content module.
 */
function resourcehub_form_install_configure_submit($form, FormStateInterface $form_state) {
  $installDemo = $form_state->getValue('install_demo');
  \Drupal::state()->set('resourcehub_demo', $installDemo);
}

/**
 * Implements hook_install_tasks_alter().
 */
function resourcehub_install_tasks_alter(&$tasks, $install_state): void {
  $tasks['resourcehub_install_content'] = [];
  $tasks['resourcehub_install_index_content'] = [];
}

/**
 * Install task callback.
 *
 * Install demo or default content depending on install configuration.
 *
 * @see resourcehub_install_tasks_alter
 * @see resourcehub_form_install_configure_form_alter
 */
function resourcehub_install_content() {
  $isDemo = \Drupal::state()->get('resourcehub_demo', FALSE);
  if ($isDemo) {
    \Drupal::service('module_installer')->install(['resourcehub_demo']);

    user_login_finalize(user_load_by_name('siteadmin'));
  }
  else {
    // Create some default taxonomy terms.
    $defaultVocabs = [
      'resourcehub_audience' => [
        'Content designers/authors',
        'User experience (UX) designers',
        'Visual designers',
      ],
      'resourcehub_resource_type' => ['Guidance', 'Advice', 'Templates'],
      'resourcehub_format' => ['Audio', 'Video', 'Blended content'],
    ];
    foreach ($defaultVocabs as $vid => $termNames) {
      foreach ($termNames as $name) {
        Term::create(['name' => $name, 'vid' => $vid])->save();
      }
    }
  }
}

/**
 * Install task callback.
 *
 * Index the resources index so the search page is immediately usable.
 *
 * @see resourcehub_install_tasks_alter
 */
function resourcehub_install_index_content() {
  $index_storage = \Drupal::entityTypeManager()
    ->getStorage('search_api_index');
  /** @var \Drupal\search_api\Entity\Index $index */
  $index = $index_storage->load('resource_index');
  $index->indexItems();
}

/**
 * Implements hook_preprocess_HOOK().
 *
 * Add the config class to the resource hub settings menu link so it
 * uses the cog icon in the toolbar.
 */
function resourcehub_preprocess_menu(&$variables) {
  if ($variables['menu_name'] == 'admin' && isset($variables['items']['resourcehub.settings'])) {
    /** @var \Drupal\Core\Url $url */
    $url = $variables['items']['resourcehub.settings']['url'];
    $url->mergeOptions(['attributes' => ['class' => ['toolbar-icon-system-admin-config']]]);
  }
}

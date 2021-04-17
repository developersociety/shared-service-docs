<?php

namespace Drupal\resourcehub\Form;

use Drupal\Core\Form\ConfigFormBase;
use Drupal\Core\Form\FormStateInterface;

/**
 * Configure Resource Hub settings for this site.
 */
class SettingsForm extends ConfigFormBase {

  /**
   * {@inheritdoc}
   */
  public function getFormId() {
    return 'resourcehub_settings';
  }

  /**
   * {@inheritdoc}
   */
  protected function getEditableConfigNames() {
    return ['resourcehub.settings', 'system.site'];
  }

  /**
   * {@inheritdoc}
   */
  public function buildForm(array $form, FormStateInterface $form_state) {
    $form['site_name'] = [
      '#type' => 'textfield',
      '#title' => $this->t('Site name'),
      '#required' => TRUE,
      '#description' => $this->t("Edit the 'site name' displayed in the header of every page."),
      '#default_value' => $this->config('system.site')->get('name'),
    ];
    $form['legal'] = [
      '#type' => 'textarea',
      '#title' => $this->t('Legal copy'),
      '#description' => $this->t("Edit the 'legal' text that appears in the footer of every page."),
      '#default_value' => $this->config('resourcehub.settings')->get('legal'),
    ];
    $form['main_site_link'] = [
      '#type' => 'fieldset',
      '#title' => 'Back to site link',
      '#tree' => TRUE,
      'intro' => [
        '#markup' => "<p>The link to your organisation's website, this will display in the header of every page. Keep the <i>Text</i> short.</p>",
        '#weight' => '-100',
      ],
    ];
    $form['main_site_link']['text'] = [
      '#type' => 'textfield',
      '#title' => "Text",
      '#description' => $this->t("The link text for the 'Back to site' link."),
      '#default_value' => $this->config('resourcehub.settings')->get('main_site_link.text'),
    ];
    $form['main_site_link']['url'] = [
      '#type' => 'url',
      '#title' => "URL",
      '#description' => $this->t("The URL for the 'Back to site' link, typically the URL of your organisation's website."),
      '#default_value' => $this->config('resourcehub.settings')->get('main_site_link.url'),
    ];
    return parent::buildForm($form, $form_state);
  }

  /**
   * {@inheritdoc}
   */
  public function submitForm(array &$form, FormStateInterface $form_state) {
    $this->config('resourcehub.settings')
      ->set('legal', $form_state->getValue('legal'))
      ->set('main_site_link', $form_state->getValue('main_site_link'))
      ->save();
    $this->config('system.site')
      ->set('name', $form_state->getValue('site_name'))
      ->save();
    parent::submitForm($form, $form_state);
  }

}

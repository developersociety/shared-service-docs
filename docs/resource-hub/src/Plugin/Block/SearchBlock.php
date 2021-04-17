<?php

namespace Drupal\resourcehub\Plugin\Block;

use Drupal\Core\Block\BlockBase;

/**
 * Provides a search block.
 *
 * @Block(
 *   id = "resourcehub_search",
 *   admin_label = @Translation("Search"),
 *   category = @Translation("Resource hub")
 * )
 */
class SearchBlock extends BlockBase {

  /**
   * {@inheritdoc}
   */
  public function build() {
    $build['content'] = [
      '#theme' => 'resourcehub_search',
    ];
    return $build;
  }

}

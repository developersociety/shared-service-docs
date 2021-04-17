<?php

$config = json_decode(file_get_contents('config.json'), true);

$repositories = $config['repositories'];

exec('rm -rf docs');
exec('mkdir docs');

$sidebar = '';

foreach ($repositories as $repository) {
    exec('git clone '.$repository['gitUrl'].' temp');
    exec('rm -rf temp/.git');
    exec('mv ./temp/'.trim($repository['sourceDocsDirectory'], '/').' ./docs/'.$repository['outputDirectory']);
    exec('rm -rf temp');

    // Repo title
    $sidebar .= '- '.$repository['outputDirectory'].PHP_EOL;

    $recursiveDirectoryIterator = new RecursiveDirectoryIterator('./docs/'.$repository['outputDirectory']);
    $recursiveIteratorIterator = new RecursiveIteratorIterator($recursiveDirectoryIterator);
    $regexIterator = new RegexIterator($recursiveIteratorIterator, '/.*\.md/i', RecursiveRegexIterator::GET_MATCH);

    // Repo items
    foreach ($regexIterator as [$fileName]) {
        if ($fileName !== mb_strtolower($fileName)) {
            exec('mv "'.$fileName.'" "'.mb_strtolower($fileName).'"');
            $fileName = mb_strtolower($fileName);
        }
        $sidebar .= '  - ['.substr($fileName, mb_strlen('./docs/'.$repository['outputDirectory'].'/'), -3).']('.substr($fileName, mb_strlen('./docs/'), -mb_strlen('.md')).')'.PHP_EOL;
    }
}

$index = <<<INDEX
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Document</title>
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
  <meta name="description" content="Description">
  <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0">
  <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/docsify@4/lib/themes/vue.css">
</head>
<body>
  <div id="app"></div>
  <script>
    window.\$docsify = {
      name: 'Docs',
      repo: '',
      loadSidebar: true,
    }
  </script>
  <!-- Docsify v4 -->
  <script src="//cdn.jsdelivr.net/npm/docsify@4"></script>
</body>
</html>
INDEX;

file_put_contents('docs/.nojekyll', '');
file_put_contents('docs/_sidebar.md', $sidebar);
file_put_contents('docs/README.md', '# Docs');
file_put_contents('docs/index.html', $index);

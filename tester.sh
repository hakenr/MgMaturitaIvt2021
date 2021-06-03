#!/bin/bash
set -euo pipefail

# Tester for comparing files with original using custom COMPARE_SCRIPT. All
# original files are located (using find . and ORIG_EXT extension). Then for
# every original file, all files with the same name (but without ORIG_EXT
# extension) are found (again using find .). Each is compared to the original.

ORIG_EXT="orig"
RECIPE_EXT="recipe"
COMPARE_SCRIPT="./BmpCompare/bin/Debug/net5.0/BmpCompare.exe"

# It is guarantied that paths won't contain spaces or other annoying
# characters. It could be dealt with but since students will need to read this
# code, it would obfuscate it unneccessarilly 
# => shellcheck disable=SC2044

mkdir -p out

# First run program on all recipes
# shellcheck disable=SC2044
for RECIPE in $(find . -name "*.$RECIPE_EXT")
do
  echo $RECIPE 
  ./BmpPlechovka/bin/Debug/net5.0/BmpPlechovka.exe < $RECIPE
done

echo
echo "========================================="
echo

INDENT=40

# Assume true until proven otherwise
ALL_TESTS_PASSED=true

# Loop through all answers
# shellcheck disable=SC2044
for ORIG_PATH in $(find . -name "*.$ORIG_EXT")
do
  echo "Testing $ORIG_PATH"

  # Assume true until proven otherwise
  NO_FILE_FOUND=true

  # Loop through all files that could be
  TESTED_FILE="$(basename -- "${ORIG_PATH%.*}")"
  # shellcheck disable=SC2044
  for TESTED_PATH in $(find . -name "$TESTED_FILE")
  do
    printf "   %-${INDENT}s " "$TESTED_PATH"

    NO_FILE_FOUND=false

    # Test if images on given paths are equal
    SUCCESS=true
    $COMPARE_SCRIPT "$ORIG_PATH" "$TESTED_PATH" || SUCCESS=false

    # Print result
    $SUCCESS && echo OK && continue
    echo FAILED
    ALL_TESTS_PASSED=false
  done


  # Skip if any file was found
  $NO_FILE_FOUND || continue
  printf "   %*s NO FILE FOUND\\n" "$INDENT" ""
  ALL_TESTS_PASSED=false
done

# Print summary
echo
$ALL_TESTS_PASSED && echo "Passed all tests" && exit 0
echo "Failed" && exit 1

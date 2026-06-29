# Gate 8.1 buildfix 2 — Icon picker wheel zone direction

## Changes

- The log dialog wheel routing from buildfix 1 is preserved unchanged.
- The Voice Changer preset icon picker no longer extends its mouse-wheel zone to the left.
- The icon picker wheel zone now extends to the right by 50% and down by 100%.
- The parent Voice Changer scroll routing remains suppressed while the icon picker is open.

## Why

The previous buildfix extended the icon picker zone to the wrong side. The visual picker needs extra wheel coverage to the right side of the popup, not to the left.

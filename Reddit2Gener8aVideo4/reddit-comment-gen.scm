(define (reddit-gen w h fn userVal head01Val head02Val cntentVal)
(let* (
	(userExt (car (gimp-text-get-extents-fontname userVal 12 PIXELS "IBM Plex Sans Medium")))
	(head01Ext (car (gimp-text-get-extents-fontname head01Val 12 PIXELS "IBM Plex Sans Medium")))
	(topbarExt (car (gimp-text-get-extents-fontname topbarVal 12 PIXELS "IBM Plex Sans Medium")))
	
	(image (car (gimp-image-new w h RGB)))
	(bg (car (gimp-layer-new image w h RGB-IMAGE "bg" 100 0)))
	(upvote (car (gimp-file-load-layer 1 image "upvote.jpg" )))
	(downvote (car (gimp-file-load-layer 1 image "downvote.jpg" )))
	(bottom (car (gimp-file-load-layer 1 image "bottombar-comment.jpg" )))
)

(gimp-context-set-background '(255 255 255))
(gimp-drawable-fill bg FILL-WHITE)
(gimp-image-insert-layer image bg 0 0)

(gimp-image-insert-layer image upvote 0 0)
(gimp-image-insert-layer image downvote 0 0)
(gimp-image-insert-layer image bottom 0 0)
(gimp-layer-translate upvote 14 10)
(gimp-layer-translate downvote 18 35)
(gimp-layer-translate bottom 45 (- h 42))

(gimp-context-set-foreground '(0 0 0))
(gimp-text-fontname image -1 77 10 subredVal 0 TRUE 12 PIXELS "IBM Plex Sans Bold")
(define content (car (gimp-text-fontname image -1 47 33 cntentVal 0 TRUE 14 PIXELS "IBM Plex Sans Medium")))
(gimp-text-layer-resize content (- w 57) (- h 67))

(gimp-context-set-foreground '(120 124 126))
(gimp-text-fontname image -1 (+ subredExt 81) 10 topbarVal 0 TRUE 12 PIXELS "IBM Plex Sans Medium")

(gimp-display-new image)
(gimp-file-save RUN-NONINTERACTIVE image (car (gimp-image-flatten image)) fn fn)
(gimp-quit TRUE)
))
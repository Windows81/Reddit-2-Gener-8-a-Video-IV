(define (reddit-comment-gen w h fn userVal head01Val head02Val cntentVal)
(let* (
	(userExt (car (gimp-text-get-extents-fontname userVal 12 PIXELS "IBM Plex Sans Medium")))
	(head01Ext (car (gimp-text-get-extents-fontname head01Val 12 PIXELS "IBM Plex Sans Medium")))
	
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
(gimp-layer-translate bottom 43 (- h 40))

(gimp-context-set-foreground '(0 0 0))
(define content (car (gimp-text-fontname image -1 47 31 cntentVal 0 TRUE 14 PIXELS "IBM Plex Sans Medium")))
(gimp-text-layer-resize content (- w 69) (- h 77))

(gimp-context-set-foreground '(68 78 89))
(gimp-text-fontname image -1 47 10 userVal 0 TRUE 12 PIXELS "IBM Plex Sans Medium")

(gimp-context-set-foreground '(124 124 124))
(gimp-text-fontname image -1 (+ 50 userExt) 10 head01Val 0 TRUE 12 PIXELS "IBM Plex Sans Medium")
(gimp-text-fontname image -1 (+ 53 userExt head01Ext) 10 head02Val 0 TRUE 12 PIXELS "IBM Plex Sans Medium Italic")

(gimp-display-new image)
(gimp-file-save RUN-NONINTERACTIVE image (car (gimp-image-flatten image)) fn fn)
(gimp-quit TRUE)
))
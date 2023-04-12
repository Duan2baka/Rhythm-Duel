```flow 
start=>start: Start action
cond1=>condition: player in front?
skill3=>operation: use skill 3
cond2=>condition: skill 4 can be used?
skill4=>operation: use skill 4
cond3=>condition: skill 2 can be used?
skill2=>operation: use skill2
cond4=>condition: skill 1 can be used?
skill1=>operation: use skill1
cond5=>condition: on same row?
movement1=>operation: Move vertically
movement2=>operation: Approach the player


end=>end: End action

start->cond1
cond1(yes)->skill3
cond1(no)->cond2
cond2(yes)->skill4
cond2(no)->cond3
cond3(yes)->skill2
cond3(no)->cond4
cond4(yes)->skill1
cond4(no)->cond5
cond5(no)->movement1
cond5(yes)->movement2

skill4->end
skill3->end
skill2->end
skill1->end
movement1->end
movement2->end
```
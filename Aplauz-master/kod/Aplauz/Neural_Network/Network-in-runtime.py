from keras.models import Sequential
import numpy as np
import sys
from keras.layers import Dense, Activation, Flatten
from keras.models import load_model
import os

import sys, os
    #print 
ruchy = sys.argv[1].split(',')

stanGry = sys.argv[2].split(',')

mozliwe_ruchy=np.array(ruchy,dtype=float)
stan_gry=np.array(stanGry,dtype=float)

stan_gry=stan_gry.astype('float32')
mozliwe_ruchy=mozliwe_ruchy.astype('float32')

current_dir=os.path.dirname(os.path.abspath(sys.argv[0]))
model = load_model(current_dir+'\modelDlugieStanyPoprawne.h5')
print(mozliwe_ruchy)
print("ugabuga")

najlepszy=(-200)
# testX=stan_gry
# np.append(testX,mozliwe_ruchy[0])
# print(testX)
for a in reversed(mozliwe_ruchy):
    if a==27:
        continue
    tmp=stan_gry
    tmp=np.append(tmp,a)
    testX=tmp

    testX=testX.reshape(1,1,146)
    testX = testX.astype('float32')
    b = model.predict(testX, batch_size=1)
    print("wynik:")
    print(b)
    if b[0]>najlepszy:
        najlepszy=a

if najlepszy==(-200):
    najlepszy=27.0
print("Wybierz ruch: "+str(najlepszy))




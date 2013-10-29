import sys, re

def check_is_datetime(line):
	return re.match('\d{2}.\d{2}.\d{4}', line) is not None

def get_datetime(line):
	vals = re.findall('\d+', line)
	return 'new DateTime({0[2]}, {0[1]}, {0[0]})'.format(vals)

def read_bands (input_stream):
	string = input_stream.readline()
	datetime = ''
	if(check_is_datetime(string)):
		datetime = get_datetime(string)



read_bands(sys.stdin)	
